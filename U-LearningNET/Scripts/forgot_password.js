document.addEventListener('DOMContentLoaded', () => {
    const form = document.getElementById('forgotPasswordForm');
    const phoneInput = document.getElementById('phoneNumber');

    // Function to validate Egyptian phone number
    function validateEgyptianPhone(phone) {
        // Egyptian phone number format: 01xxxxxxxxx (11 digits)
        const phoneRegex = /^01[0125][0-9]{8}$/;
        return phoneRegex.test(phone);
    }

    // Handle phone number input formatting
    phoneInput.addEventListener('input', (e) => {
        // Remove any non-numeric characters
        e.target.value = e.target.value.replace(/[^0-9]/g, '');
        
        // Limit to 11 digits
        if (e.target.value.length > 11) {
            e.target.value = e.target.value.slice(0, 11);
        }
    });

    // Handle form submission
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        
        const phoneNumber = phoneInput.value;

        // Validate phone number
        if (!validateEgyptianPhone(phoneNumber)) {
            alert('يرجى إدخال رقم هاتف مصري صحيح');
            return;
        }

        // Store the phone number in sessionStorage for the verification page
        sessionStorage.setItem('userPhone', phoneNumber);

        // Here you would typically make an API call to send the verification code
        // For demo purposes, we'll just redirect to the verification page
        window.location.href = 'phoneVerification.html';
    });
}); 