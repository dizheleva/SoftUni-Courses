document.addEventListener('DOMContentLoaded', solve);

function solve() {
    const correctAnswers = ["onclick", "JSON.stringify()", "A programming API for HTML and XML documents"];

    let points = 0;
    const questions = Array.from(document.getElementsByTagName('section'));

    for (let i = 0; i < questions.length; i++) {
        const question = questions[i];
        const answers = Array.from(question.querySelectorAll('.quiz-answer'));

        answers.forEach(answer => answer.addEventListener('click', (e) => {
            if (correctAnswers.includes(e.target.textContent)) {
                points++;
            };

            question.classList.add('hidden');

            if (i < questions.length - 1) {
                questions[i + 1].classList.remove('hidden');
            } else {
                displayResult(points);
            }
        }));
    }

    function displayResult(points) {
        let text = '';
        switch (points) {
            case questions.length:
                text = 'You are recognized as top JavaScript fan!';
                break;

            case 1:
                text = `You have ${points} right answer`;
                break;

            default:
                text = `You have ${points} right answers`;
                break;
        }

        const resultDiv = document.querySelector('#results');
        const resultH1 = document.createElement('h1');
        resultH1.textContent = text;
        resultDiv.appendChild(resultH1);
        resultDiv.style.display = 'block';
    }
}