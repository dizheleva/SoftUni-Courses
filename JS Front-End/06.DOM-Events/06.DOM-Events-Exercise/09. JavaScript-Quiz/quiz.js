document.addEventListener('DOMContentLoaded', solve);

function solve() {
    let points = 0;

    let currSection = 0;
    const questions = Array.from(document.getElementsByTagName('section'));
    const correctAnswers = ["onclick", "JSON.stringify()", "A programming API for HTML and XML documents"];

    for (let i = 0; i < questions.length; i++) {
        const question = questions[i];

        if (question.hasClassName('hidden') == true) {
            question.style.display = 'none';
        } else {
            if (i != question.length - 1) {
                questions[i + 1].style.display = 'block';
            }
        }
        const answers = Array.from(question.querySelectorAll('.quiz-answer'));

        answers.forEach(answer => answer.addEventListener('click', (e) => {
            if (correctAnswers.includes(e.target.textContent)) {
                points++;
            };

            question.classList.add('hidden');
            if (question.hasClassName('hidden') == true) {
                question.style.display = 'none';
            } else {
                if (i != question.length - 1) {
                    questions[i + 1].style.display = 'block';
                }
            }
        }));

        
    }

    if (questions.includes(question => question.hasClassName('hidden') == false)) {
        document.querySelector('#results').style.display = 'block';
        let text = '';
        points === 3
            ? text = 'You are recognized as top JavaScript fan!'
            : text = `You have ${points} right answers`;
        document.querySelector('#results').textContent = text;
    }

}