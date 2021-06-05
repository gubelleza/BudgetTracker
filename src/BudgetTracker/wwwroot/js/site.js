function toggleEdition(index) {
    let displayElements = document.getElementsByClassName("display-info-" + index);
    let editionElements = document.getElementsByClassName("edit-info-" + index);   

    for (i = 0; i < displayElements.length; i++) {
        let element = displayElements.item(i);
        console.log(element.className);

        if (element.hasAttribute("hidden"))
            element.removeAttribute("hidden")
        else
            element.setAttribute("hidden", "true")
    }

    for (i = 0; i < editionElements.length; i++) {
        let element = editionElements.item(i);
        console.log(element.className);

        if (element.hasAttribute("hidden"))
            element.removeAttribute("hidden")
        else
            element.setAttribute("hidden", "true")
    }
}

let editionTriggers = document.getElementsByClassName("edition-trigger");

for (i = 0; i < editionTriggers.length; i++) {
    let element = editionTriggers.item(i);
    console.log(element.className);
    element.addEventListener("click", () => {
        let trigerIndex = element.id;
        toggleEdition(trigerIndex);
    });
}

