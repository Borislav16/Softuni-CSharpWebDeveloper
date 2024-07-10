function solve(input){
    let cities = input.map((city)=>{
        let [name, lat, long] = city.split(" | ");
        return {towns: name, latitude: lat, longtitude: long};
    })

    for(let city of cities){
        city.latitude = Number(city.latitude).toFixed(2);
        city.longtitude = Number(city.longtitude).toFixed(2);
        console.log(`{ town: '${city.towns}', latitude: '${city.latitude}', longitude: '${city.longtitude}' }`)
    }
}