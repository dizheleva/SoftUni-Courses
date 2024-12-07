function solve() {
    const searched = document.querySelector('#searchField').value.toLowerCase().trim();
    const students = document.querySelectorAll('table tbody tr');

    if (searched != '') {
        students.forEach(student => {
            student.classList.remove('select');

            if (student.textContent.toLowerCase().includes(searched)) {
                student.classList.add('select');
            }
        });

        document.querySelector('#searchField').value = '';
    }
}