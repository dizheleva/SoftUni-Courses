function passwordValidator(password) {
    function hasValidCharacterLength(password) {
        if (password.length < 6 || password.length > 10) {
            return false;
        }

        return true;
    }

    function isAlphanumeric(password) {
        for (let i = 0; i < password.length; i++) {
            const charCode = password.charCodeAt(i);

            if (!(charCode >= '0'.charCodeAt(0) && charCode <= '9'.charCodeAt(0))
                && !(charCode >= 'A'.charCodeAt(0) && charCode <= 'Z'.charCodeAt(0))
                && !(charCode >= 'a'.charCodeAt(0) && charCode <= 'z'.charCodeAt(0))) {
                return false;
            }
        }

        return true;
    }

    function hasTwoDigits(password) {
        let digitCount = 0;

        for (const character of password) {
            let digit = Number(character);

            if (Number.isInteger(digit)) {
                digitCount++;
            }
        }

        if (digitCount >= 2) {
            return true;
        } else {
            return false;
        }
    }

    let isValid = true;

    if (!hasValidCharacterLength(password)) {
        console.log('Password must be between 6 and 10 characters');
        isValid = false;
    }

    if (!isAlphanumeric(password)) {
        console.log('Password must consist only of letters and digits');
        isValid = false;
    }

    if (!hasTwoDigits(password)) {
        console.log('Password must have at least 2 digits');
        isValid = false;
    }

    if (isValid) {
        console.log('Password is valid');
    }
}

passwordValidator('logIn');
passwordValidator('MyPass123');
passwordValidator('Pa$s$s');