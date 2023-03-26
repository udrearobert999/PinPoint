const navAvatar = document.getElementById('nav-avatar');
const navAvatarDropDown = document.getElementById('nav-avatar-dropdown')

window.addEventListener('click', (e) => {
    if (!navAvatar.contains(e.target))
        navAvatarDropDown.hidden = true;
})

navAvatar.addEventListener('click', () => {
    navAvatarDropDown.toggleAttribute('hidden');
})

document.getElementById('logout-action').addEventListener('click', async () => {
    const respone = await fetch(`/Identity/Account/Logout`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/x-www-form-urlencoded',
            'RequestVerificationToken': getAntiForgeryToken()
        },
    })

    if (respone.ok)
        window.location.href = '/'; // redirect to main page
})