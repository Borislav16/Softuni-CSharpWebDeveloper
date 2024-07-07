function largest(first, second, third){
    if(first > second && first > third)
    {
        console.log(`The largest number is ${first}.`)
    }
    else if(second > first && second > third)
    {
        console.log(`The largest number is ${second}.`)
    }
    else{
        console.log(`The largest number is ${third}.`)
    }
}

