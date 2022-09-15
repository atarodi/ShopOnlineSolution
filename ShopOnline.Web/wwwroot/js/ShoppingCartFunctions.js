function MakeUpdateQtyButtonVisible(id, visible) {
    const updateQtyButton = document.querySelector("button[data-itemId='" + id + "']");

    if (visible == true) {
        updateQtyButton.style.display = "inline-block";
    }
    else {
        updateQtyButton.style.display = "none";
    }


}

//-----------------------
window.dotNetToJsSamples = {
    getWeatherData: function () {
        return fetch('/sample-data/weather.json')
            .then(response => response.json());
    }
   

};
//------------------------------
window.dotNetToJsSamples2 = {
    getRandomData: function () {
        var firstDate = new Date.now();
        fetch('https://localhost:7289/api/Analysis/GetRandomData/1000')
            .then(response =>
            {
                response.json();
                var secondDate = new Date.now();
                alert(firstDate.getTime() - secondDate.getTime());
            });
       


    }

};
