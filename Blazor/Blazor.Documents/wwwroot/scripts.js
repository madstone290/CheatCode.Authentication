
var name = null;

export function showPrompt(message) {
    if (!name) {
        name = prompt(message, "empty");
    } else {
        name = prompt(message, name);
    }
    return name;
}