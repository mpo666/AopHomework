export default function CbCurrency(props: object) {
    if (!Array.isArray(props.currencies)) {
        props.currencies = [];
    }
    return (
        <div>
            <label htmlFor={props.id}>Currency: </label>
            <select className="form-select form-select-lg mb-3" id={props.id} onChange={props.onchange} >
                {props.currencies.map(currency => <option key={currency.name}>{currency.name}</option>)}
            </select>
        </div>
    );
}

//export default CbCurrency;