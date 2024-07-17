function attachEventsListeners() {

    let convertBtn = document.getElementById('convert');
    convertBtn.addEventListener('click', convert);

    let units = {
        km: 1000,
        m: 1,
        cm: 0.01,
        mm: 0.001,
        mi: 1609.34,
        yrd: 0.9144,
        ft: 0.3048,
        in: 0.0254
    }


    function convert() {

        let inputValue = Number(document.getElementById('inputDistance').value);


        let inputUnit = document.getElementById('inputUnits').value;

        let outputUnit = document.getElementById('outputUnits').value;


        let inputValueToM = inputValue * units[inputUnit];

        let outputResult = inputValueToM / units[outputUnit];
        
        let outputValue = document.getElementById('outputDistance');
        outputValue.value = outputResult;
    }       
}