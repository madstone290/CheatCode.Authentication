export function addCookie(cookieText) {
    console.log("add cookie: " + cookieText);
    
    document.cookie = "name=oeschger; SameSite=None; Secure";
    document.cookie = cookieText;
}
export function readCookie() {
    var decodedCookie = decodeURIComponent(document.cookie);
    return decodedCookie;
}


export function SignIn(username, password, redirect) {
    console.log("SignIn......");
    var url = "https://localhost:5003/auth/loginjson";
    var xhr = new XMLHttpRequest();

    // Initialization
    xhr.open("POST", url);
    xhr.setRequestHeader("Accept", "application/json");
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.withCredentials = true;

    // Catch response
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) // 4=DONE 
        {
            console.log("Call '" + url + "'. Status " + xhr.status);
            if (redirect)
                location.replace(redirect);
        }
    };

    // Data to send
    var data = {
        username: username,
        password: password
    };

    // Call API
    xhr.send(JSON.stringify(data));
}

export function SignOut(logoutId, redirect) {

    var url = "https://localhost:5003/account/logout?logoutId=" + logoutId;
    var xhr = new XMLHttpRequest();

    // Initialization
    xhr.open("POST", url);
    xhr.setRequestHeader("Accept", "application/json");
    xhr.setRequestHeader("Content-Type", "application/json");

    // Catch response
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) // 4=DONE 
        {
            console.log("Call '" + url + "'. Status " + xhr.status);
            if (redirect)
                location.replace(redirect);
        }
    };

    // Call API
    xhr.send();
}
