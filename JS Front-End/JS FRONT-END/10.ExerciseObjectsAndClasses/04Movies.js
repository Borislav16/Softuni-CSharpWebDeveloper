function movies(input) {
    let moviesArr = [];
    for (const moviesArrElement of input) {
        if (moviesArrElement.includes("addMovie")) {
            const [_, name] = moviesArrElement.split("addMovie ");
            moviesArr.push({
                name
            })

        } else if (moviesArrElement.includes("directedBy")) {
            const [movieName, dictorName] = moviesArrElement.split(" directedBy ");
            const movie = moviesArr.find(m => m.name === movieName);
            if (movie) {
                movie.director = dictorName;
            }
        } else if (moviesArrElement.includes("onDate")) {
            const [movieName, movieDate] = moviesArrElement.split(" onDate ");
            const movie = moviesArr.find(m => m.name === movieName);
            if (movie) {
                movie.date = movieDate;
            }

        }
    }

    moviesArr
        .filter(movie => movie.name && movie.director && movie.date)
        .forEach(movie => console.log(JSON.stringify(movie)));
}