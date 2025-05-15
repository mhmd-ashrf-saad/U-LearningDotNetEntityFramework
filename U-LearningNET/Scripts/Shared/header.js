document.addEventListener('DOMContentLoaded', function() {
    const hamburger = document.querySelector('.hamburger');
    const offScreenMenu = document.querySelector('.off-screen-menu');
    const body = document.body;

    if (hamburger && offScreenMenu) {
        hamburger.addEventListener('click', function(e) {
            e.stopPropagation();
            hamburger.classList.toggle('active');
            offScreenMenu.classList.toggle('active');
            body.style.overflow = offScreenMenu.classList.contains('active') ? 'hidden' : '';
        });

        // Close menu when clicking outside
        document.addEventListener('click', function(event) {
            if (!hamburger.contains(event.target) && !offScreenMenu.contains(event.target)) {
                hamburger.classList.remove('active');
                offScreenMenu.classList.remove('active');
                body.style.overflow = '';
            }
        });

        // Close menu when clicking on a link
        const menuLinks = offScreenMenu.querySelectorAll('a');
        menuLinks.forEach(link => {
            link.addEventListener('click', function() {
                hamburger.classList.remove('active');
                offScreenMenu.classList.remove('active');
                body.style.overflow = '';
            });
        });
    }
});