document.addEventListener('DOMContentLoaded', solve);

function solve() {
    const sections = Array.from(document.querySelectorAll('.panel'));

    sections.forEach(panel => {
        const input = panel.querySelector('input');

        input.addEventListener('focus', () => panel.classList.add('focused'));        
        input.addEventListener('blur', () => panel.classList.remove('focused'));
    
    });
}