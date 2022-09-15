window.dotNetToJsSamples3 = {
    getRandomData3: function () {
        var firstDate = Date.now();
       var randomList= fetch('https://localhost:7289/api/Analysis/GetRandomData/1000')
            .then(response => {
                response.json();
                var secondDate = Date.now();
                console.log(secondDate - firstDate);
                return secondDate - firstDate;
            });
       
        

    }

};

