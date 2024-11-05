function solve(base, increment) {
    let steps = Math.round(base / 2);
    let height = Math.floor(steps * increment);
    let gold = base % 2 === 0 ? 4 * increment : increment;
    let stone = 0; 
    let marble = 0
    let lapisLazuli = 0;
    let size = base;

    for (let i = 1; i < steps; i++) {
        stone += Math.pow(size - 2, 2) * increment;
        if (i % 5 === 0) {
            lapisLazuli += increment * 4 * (size - 1);
        } else {
            marble += increment * 4 * (size - 1);
        }     
        
        size -= 2;
    }
        
    console.log(`Stone required: ${Math.ceil(stone)}`);
    console.log(`Marble required: ${Math.ceil(marble)}`);
    console.log(`Lapis Lazuli required: ${Math.ceil(lapisLazuli)}`);
    console.log(`Gold required: ${Math.ceil(gold)}`);
    console.log(`Final pyramid height: ${height}`);
}

solve(11, 1);
solve(11, 0.75);
solve(12, 1);
solve(23, 0.5);