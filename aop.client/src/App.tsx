import { useEffect, useState } from 'react';
import CbCountries from './components/CbCountries';
import CbCurrency from './components/CbCurrency';
import TbText from './components/TbText';
import Currency from './interfaces/Currency';
import Country from './interfaces/Country';

import './App.css';

function App() {

    const [countries, setCountries] = useState<Country[]>();
    const [currencies, setCurrencies] = useState<Currency[]>();

    useEffect(() => {
        loadCountries();
        loadCurrencies();
    }, []);


    let contents = <div>loading data...</div>;
    if (countries !== undefined && currencies !== undefined) {

        contents = <div>
            <CbCountries label={'Country'} id={'country'} countries={countries} onchange={onChangeParams} />
            <CbCurrency label={'Currency'}  id={'currency'} currencies={currencies} onchange={onChangeParams} />
            <br />
            <TbText id={'avgOrder'} label={'Avg. order costs:'} />
            <TbText id={'avgFreight'} label={'Avg. freight costs:'} />
        </div>;
            
    }


    return (
        <div>
            <h1>Country AOP</h1>
            {contents}
        </div>
    );

    async function onChangeParams() {
        const country = document.getElementById('country');
        const currency = document.getElementById('currency');

        const avgOrder = document.getElementById('avgOrder');
        const avgFreight = document.getElementById('avgFreight');

        if (country.value == '' || currency.value == '') {
            alert('please select country and currency');
            avgOrder.value = '';
            avgFreight.value = '';
            return;
        }

        const url = 'api/aop/' + currency.value + '/' + country.value;
        const response = await fetch(url);
        const data = await response.json();

        if (!Array.isArray(data) || data.length <= 0) {
            alert('exchange data missing');
            avgOrder.value = '';
            avgFreight.value = '';
            return;
        }
        avgOrder.value = data[0].avgOrder.toFixed(2) + " " + data[0].exchangeCurrency;
        avgFreight.value = data[0].avgFreight.toFixed(2) + " " + data[0].exchangeCurrency;
    }

    async function loadCountries() {
        const response = await fetch('api/countries');
        const data = await response.json();
        setCountries(data);
    }
    async function loadCurrencies() {
        const response = await fetch('api/currencies');
        const data = await response.json();
        //const data = [];
        setCurrencies(data);
    }
}

export default App;