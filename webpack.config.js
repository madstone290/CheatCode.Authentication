const path = require('path');
const glob = require('glob');
const webpack = require('webpack')

function toPosixPath(windowPath) {
    return windowPath.replace(/\\/g, "/")
}

function createEntryForAllJsFiles(subDirWindowPath) {
    const regexWindow = path.join(__dirname, subDirWindowPath, String.raw`**\*.ts`);
    const regexPosix = toPosixPath(regexWindow);
    const files = glob.sync(regexPosix).map(windowPath => { return toPosixPath(windowPath); });

    const entry = files.reduce(function (obj, file) {
        const subDirPosixPath = toPosixPath(subDirWindowPath);
        const nameIdx = file.lastIndexOf(subDirPosixPath) + subDirPosixPath.length;
        const name = file.substring(nameIdx).replace(".ts", "");
        obj[name] = file;
        return obj
    }, {});
    return entry;
}


/* ./Pages 디렉토리 안에 있는 ts파일에 대해서만 엔트리를 생성한다 */
const entry = createEntryForAllJsFiles(String.raw`Pages`)

module.exports = {
    mode: "development",
    entry: entry,
   /* 번들 파일 경로 설정
   ex) Pages/Item/Index.ts -> wwwroot/dist/Item.js
    */
    output: {
        path: path.resolve(__dirname, "wwwroot/dist"),
        filename: "[name].js",
        clean: true
    },
    resolve: {
        extensions: [".ts"],
        alias: {
            "Pages": path.resolve(__dirname, "Pages/"),
            "Scripts": path.resolve(__dirname, "Scripts/")
        }
    },
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: /node_modules/,
            },
        ],
    },
    /* IDE에서 디버깅을 위해 소스경로를 절대 경로로 설정한다 */
    devtool: false,
    plugins: [
        new webpack.SourceMapDevToolPlugin({
            filename: "[file].map",
            fallbackModuleFilenameTemplate: '[absolute-resource-path]',
            moduleFilenameTemplate: '[absolute-resource-path]'
        })
    ],
}