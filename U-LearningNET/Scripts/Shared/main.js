// scroll up button
const scrollUpButton = document.getElementById("scrlUp");
let isScrolling = false;
let scrollTimeout;

// Debounce function to limit how often a function can be called
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

// Enhanced scroll function with debounce
const handleScroll = debounce(() => {
    const scrollPosition = window.scrollY || document.documentElement.scrollTop;
    
    if (scrollPosition > 360) {
        scrollUpButton.style.display = "block";
        // Use requestAnimationFrame for smoother animation
        requestAnimationFrame(() => {
            scrollUpButton.classList.add("visible");
        });
    } else {
        scrollUpButton.classList.remove("visible");
        // Hide the button after animation completes
        scrollTimeout = setTimeout(() => {
            scrollUpButton.style.display = "none";
        }, 300); // Match this with your CSS transition duration
    }
}, 100);

// Add scroll event listener with passive option for better performance
window.addEventListener('scroll', handleScroll, { passive: true });

// Smooth scroll to top function with faster easing
function topFunction() {
    if (isScrolling) return;
    
    isScrolling = true;
    const startPosition = window.scrollY || document.documentElement.scrollTop;
    const startTime = performance.now();
    
    function scrollStep(currentTime) {
        const elapsed = currentTime - startTime;
        const progress = Math.min(elapsed / 500, 1); // Reduced duration to 500ms
        
        // Faster easing function
        const easeInOutQuad = t => t < 0.5 ? 2 * t * t : 1 - Math.pow(-2 * t + 2, 2) / 2;
        const easedProgress = easeInOutQuad(progress);
        
        const currentScroll = startPosition * (1 - easedProgress);
        window.scrollTo(0, currentScroll);
        
        if (progress < 1) {
            requestAnimationFrame(scrollStep);
        } else {
            isScrolling = false;
        }
    }
    
    requestAnimationFrame(scrollStep);
}

// Add keyboard accessibility for scroll up button
scrollUpButton.addEventListener('keydown', (e) => {
    if (e.key === 'Enter' || e.key === ' ') {
        e.preventDefault();
        topFunction();
    }
});

// Add focus styles for better accessibility
scrollUpButton.addEventListener('focus', () => {
    scrollUpButton.classList.add('focused');
});

scrollUpButton.addEventListener('blur', () => {
    scrollUpButton.classList.remove('focused');
});

// Clean up on page unload
window.addEventListener('unload', () => {
    clearTimeout(scrollTimeout);
    window.removeEventListener('scroll', handleScroll);
});

// Handle search functionality
const searchInput = document.querySelector('.search-input');
const searchButton = document.querySelector('.search-button');

if (searchInput && searchButton) {
    searchButton.addEventListener('click', function() {
        const searchTerm = searchInput.value.trim();
        if (searchTerm) {
            // Implement search functionality here
            console.log('Searching for:', searchTerm);
        }
    });

    searchInput.addEventListener('keypress', function(e) {
        if (e.key === 'Enter') {
            const searchTerm = searchInput.value.trim();
            if (searchTerm) {
                // Implement search functionality here
                console.log('Searching for:', searchTerm);
            }
        }
    });
}

// Add error handling for images
document.addEventListener('error', (e) => {
    if (e.target.tagName.toLowerCase() === 'img') {
        e.target.src = 'image/placeholder.png'; // Add a placeholder image
        e.target.alt = 'صورة غير متوفرة';
    }
}, true);