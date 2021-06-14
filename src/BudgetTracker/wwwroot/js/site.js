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

function disableInputs(index) {
    let inputs = document.getElementsByClassName("input-row-" + index);
    for (i = 0; i < inputs.length; i++)
    {
        let element = inputs[i];
        if (element.disabled)
        {
            element.removeAttribute("disabled");
        } else {
            element.setAttribute("disabled", true);
        }
    }
}

let checks = document.getElementsByClassName("form-check");
for (i = 0; i < checks.length; i++) {
    let element = checks[i];
    element.addEventListener("click", () => {
        let checkIndex = element.id;
        disableInputs(checkIndex);
    });
}

let editionTriggers = document.getElementsByClassName("edition-trigger");
for (i = 0; i < editionTriggers.length; i++) {
    let element = editionTriggers.item(i);
    
    element.addEventListener("click", () => {
        let triggerIndex = element.id;
        toggleEdition(triggerIndex);
    });
}

