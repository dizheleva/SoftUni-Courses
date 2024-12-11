document.addEventListener('DOMContentLoaded', solve);

function solve() {
    document.querySelectorAll('form').forEach(form => form.addEventListener('submit', (e) => {
        e.preventDefault();

        const messageArea = e.target.querySelector('textarea');
        const message = messageArea.value;
        
        const action = e.target.id;

        switch (action) {
            case 'encode':
                const encoded = message.split('').map(ch => String.fromCharCode(ch.charCodeAt() + 1)).join('');

                document.querySelector('#decode textarea').value = encoded;
                messageArea.value = '';
                break;

            case 'decode':
                const decoded = message.split('').map(ch => String.fromCharCode(ch.charCodeAt() - 1)).join('');

                messageArea.value = decoded;
                break;
        }
    }))
}