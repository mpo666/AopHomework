import Currency from '../interfaces/Currency';

type CbCurrencyProps = {
    id: string;
    currencies: Currency[];
    label?: string;
    value?: string;
    onchange?: object;
};

const CbCurrency = (props: CbCurrencyProps) => {
    return (
        <div>
            <label htmlFor={props.id}>{props.label}</label>
            <select className="form-select form-select-lg mb-3" id={props.id} onChange={props.onchange}>
                {props.currencies.map(currency => <option key={currency.name}>{currency.name}</option>)}
            </select>
        </div>
    );
}

export default CbCurrency;

