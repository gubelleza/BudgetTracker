// TOGGLE EDITION FUNCTION
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
            }
            else
            {
                element.setAttribute("hidden", "true");
            }
        } 
    }
    toggleElements(displayElements);
    toggleElements(editionElements);
}

// Event Listener
let editionTriggers = document.getElementsByClassName("edition-trigger");
for (i = 0; i < editionTriggers.length; i++) {
    let element = editionTriggers.item(i);

    element.addEventListener("click", () => {
        let triggerIndex = element.id;
        toggleEdition(triggerIndex);
    });
}

// DISABLE INPUTS
function disableInputs(index) {
    let inputs = document.getElementsByClassName("input-row-" + index);
    for (i = 0; i < inputs.length; i++) {
        let element = inputs[i];
        if (element.disabled)
        {
            element.removeAttribute("disabled");
        } else {
            element.setAttribute("disabled", true);
        }
    }
}

function toggleIfClicked(element) {
    if (element.hasAttribute("checked")) {
        element.removeAttribute("checked");
    } else {
        element.setAttribute("checked", "true");
    }
}

function displayAlertIfAnyChecked(checkBoxElements) {
    if([...checkBoxElements].some(c => c.hasAttribute("checked"))) {
        document
            .getElementById("delete-alert")
            .removeAttribute("hidden");
    } else {
        document
            .getElementById("delete-alert")
            .setAttribute("hidden", "true");
    }
}

// Event Listener
let checks = document.getElementsByClassName("form-check");
for (i = 0; i < checks.length; i++) {
    let element = checks[i];
    element.addEventListener("click", () => {
        let checkIndex = element.id;
        disableInputs(checkIndex);
        toggleIfClicked(element);
        displayAlertIfAnyChecked(checks);
    });
}

// ADD MEMBER BUTTON
function displayAddMemberForm() {
    let addMemberForm = document.getElementById("add-member-form");
    
}

let addMemberButton = document.getElementById("add-member-btn");
addMemberButton.addEventListener("click", () => {
    let addMemberForm = document.getElementById("add-member-form");
    if (addMemberForm.hasAttribute("hidden"))
        addMemberForm.removeAttribute("hidden");
    else
        addMemberForm.setAttribute("hidden", "true");
})