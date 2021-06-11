function toggleEdition(index) {
    let displayElements = document.getElementsByClassName("display-info-" + index);
    let editionElements = document.getElementsByClassName("edit-info-" + index);   

    console.log("Index triggered: " + index);
    console.log("display elements: " + displayElements.length);
    console.log("edition elements: " + editionElements.length);
    
    let toggleElements = (elements) => {
        
        for (i = 0; i < elements.length; i++) {
            let element = elements.item(i);
    
            if (element.hasAttribute("hidden"))
            {
                element.removeAttribute("hidden");
                console.log("displaying: " + element.innerHTML);
            }
            else
            {
                element.setAttribute("hidden", "true");
                console.log("hiding : " + element.innerHTML);
            }
        } 
    }

    toggleElements(displayElements);
    toggleElements(editionElements);
}

let editionTriggers = document.getElementsByClassName("edition-trigger");

for (i = 0; i < editionTriggers.length; i++) {
    let element = editionTriggers.item(i);

    console.log("Item: " + element.innerHTML);

    element.addEventListener("click", () => {
        let triggerIndex = element.id;
        toggleEdition(triggerIndex);
    });
}

