function getMovieData(input) {
    let movies = [];

    for (const commandLine of input) {
        if (commandLine.includes('addMovie ')) {
            const movieName = commandLine.substring('addMovie '.length);
            const movie = { name: movieName };
            movies.push(movie);
        } else {
            const spliter = commandLine.includes('onDate') ? ' onDate ' : ' directedBy ';
            const movieInfo = commandLine.split(spliter);

            const propName = commandLine.includes('onDate') ? 'date' : 'director';
            const movieName = movieInfo[0];
            const propValue = movieInfo[1];

            const movie = movies.find(m => m.name === movieName);
            
            if (movie) {
                movie[propName] = propValue;
            }
        } 
    }

    for (const movie of movies) {
        if (movie.name && movie.director && movie.date) {
            let movieJSON = JSON.stringify(movie);
            console.log(movieJSON);
        }
    }
}

getMovieData(
    [
        'addMovie Fast and Furious',
        'addMovie Godfather',
        'Inception directedBy Christopher Nolan',
        'Godfather directedBy Francis Ford Coppola',
        'Godfather onDate 29.07.2018',
        'Fast and Furious onDate 30.07.2018',
        'Batman onDate 01.08.2018',
        'Fast and Furious directedBy Rob Cohen'
    ]
);
getMovieData(
    [
        'addMovie The Avengers',
        'addMovie Superman',
        'The Avengers directedBy Anthony Russo',
        'The Avengers onDate 30.07.2010',
        'Captain America onDate 30.07.2010',
        'Captain America directedBy Joe Russo'
    ]
);