const navAvatar = document.getElementById('nav-avatar');
const navAvatarDropDown = document.getElementById('nav-avatar-dropdown')

window.addEventListener('click', (e) => {
    if (!navAvatar.contains(e.target))
        navAvatarDropDown.hidden = true;
})

navAvatar.addEventListener('click', () => {
    navAvatarDropDown.toggleAttribute('hidden');
})

// Add event handler to every dropdown elements 
const formsSubmitButtons = document.querySelectorAll('form > .submit')
for (const button of formsSubmitButtons)
    button.addEventListener('click', () => {
        button.parentElement.submit()
    })