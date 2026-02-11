// Password Validation - Client-Side
// This file provides real-time password strength checking and requirements validation

function initializePasswordValidator(inputId, options = {}) {
    const passwordInput = document.getElementById(inputId);
    if (!passwordInput) return;

    const strengthBar = document.getElementById(options.strengthBarId || 'passwordStrengthBar');
    const strengthLabel = document.getElementById(options.strengthLabelId || 'strengthLabel');
    
    // Requirements elements
    const reqLength = document.getElementById(options.reqLengthId || 'req-length');
    const reqUppercase = document.getElementById(options.reqUppercaseId || 'req-uppercase');
    const reqLowercase = document.getElementById(options.reqLowercaseId || 'req-lowercase');
    const reqDigit = document.getElementById(options.reqDigitId || 'req-digit');
    const reqSpecial = document.getElementById(options.reqSpecialId || 'req-special');
    
    passwordInput.addEventListener('input', function() {
        const password = this.value;
        let strength = 0;
        let feedback = 'Weak';
        let barColor = 'bg-danger';
        let requirementsMet = 0;
        
        // Check length (minimum 12 characters)
        if (password.length >= 12) {
            strength += 25;
            requirementsMet++;
            if (reqLength) {
                reqLength.className = 'text-success';
                reqLength.innerHTML = '? At least 12 characters';
            }
        } else {
            if (reqLength) {
                reqLength.className = 'text-muted';
                reqLength.innerHTML = '? At least 12 characters';
            }
        }
        
        if (password.length >= 16) strength += 10;
        
        // Check for uppercase
        if (/[A-Z]/.test(password)) {
            strength += 15;
            requirementsMet++;
            if (reqUppercase) {
                reqUppercase.className = 'text-success';
                reqUppercase.innerHTML = '? Contains uppercase letter';
            }
        } else {
            if (reqUppercase) {
                reqUppercase.className = 'text-muted';
                reqUppercase.innerHTML = '? Contains uppercase letter';
            }
        }
        
        // Check for lowercase
        if (/[a-z]/.test(password)) {
            strength += 15;
            requirementsMet++;
            if (reqLowercase) {
                reqLowercase.className = 'text-success';
                reqLowercase.innerHTML = '? Contains lowercase letter';
            }
        } else {
            if (reqLowercase) {
                reqLowercase.className = 'text-muted';
                reqLowercase.innerHTML = '? Contains lowercase letter';
            }
        }
        
        // Check for numbers
        if (/\d/.test(password)) {
            strength += 15;
            requirementsMet++;
            if (reqDigit) {
                reqDigit.className = 'text-success';
                reqDigit.innerHTML = '? Contains number';
            }
        } else {
            if (reqDigit) {
                reqDigit.className = 'text-muted';
                reqDigit.innerHTML = '? Contains number';
            }
        }
        
        // Check for special characters ($ @ ! % * ? & #)
        if (/[$@!%*?&#]/.test(password)) {
            strength += 20;
            requirementsMet++;
            if (reqSpecial) {
                reqSpecial.className = 'text-success';
                reqSpecial.innerHTML = '? Contains special character';
            }
        } else {
            if (reqSpecial) {
                reqSpecial.className = 'text-muted';
                reqSpecial.innerHTML = '? Contains special character';
            }
        }
        
        // Determine strength level
        if (strength === 0) {
            feedback = 'None';
            barColor = 'bg-secondary';
        } else if (strength < 50) {
            feedback = 'Weak';
            barColor = 'bg-danger';
        } else if (strength < 75) {
            feedback = 'Medium';
            barColor = 'bg-warning';
        } else if (strength < 100) {
            feedback = 'Good';
            barColor = 'bg-info';
        } else {
            feedback = 'Strong';
            barColor = 'bg-success';
        }
        
        // Update strength bar
        if (strengthBar) {
            strengthBar.style.width = strength + '%';
            strengthBar.className = 'progress-bar ' + barColor;
        }
        if (strengthLabel) {
            strengthLabel.textContent = feedback;
            strengthLabel.className = barColor.replace('bg-', 'text-');
        }
        
        return {
            strength: strength,
            requirementsMet: requirementsMet,
            allRequirementsMet: requirementsMet === 5 && password.length >= 12
        };
    });
}
