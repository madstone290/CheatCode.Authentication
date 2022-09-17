window.returnArrayAsync = () => {
    DotNet.invokeMethodAsync('Blazor.Documents', 'ReturnArrayAsync')
        .then(data => {
            console.log(data);
        });
};


window.sayHello1 = (dotnetRef) => {
    return dotnetRef.invokeMethodAsync('GetHelloMessage');
};

window.sayHello2 = (dotnetRef, name) => {
    return dotnetRef.invokeMethodAsync('GetHelloMessage', name);
};

function createGuid(dotnetRef) {
    return dotnetRef.invokeMethodAsync('guid', 'NewGuid');
}

window.updateMessageCaller = (dotnetRef) => {
    dotnetRef.invokeMethodAsync('Blazor.Documents', 'UpdateMessageCaller');
    dotnetRef.dispose();
}

window.convertArray = (win1251Array) => {
    var win1251decoder = new TextDecoder('windows-1251');
    var bytes = new Uint8Array(win1251Array);
    var decodedArray = win1251decoder.decode(bytes);
    console.log(decodedArray);
    return decodedArray;
};

window.displayTickerAlert1 = (symbol, price) => {
    alert(`${symbol}: $${price}!`);
};


window.interopFunctions = {
    clickElement: function (element) {
        console.log(element);
        element.click();
    }
}

function setElementClass(element, className) {
    var myElement = element;
    myElement.classList.add(className);
}