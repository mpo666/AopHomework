import Country from '../interfaces/Country';

type CbCountryProps = {
    id: string;
    countries: Country[];
    label?: string;
    value?: string;
    onchange?: object;
};


const CbCountries = (props: CbCountryProps) => {
    return (
        <div>
            <label htmlFor={props.id}>{props.label}</label>
            <select id={props.id} onChange={props.onchange}>
                <option value="">please select</option>
                {props.countries.map(country => <option key={country.name} value={country.name}>{country.name}</option>)}
            </select>
        </div>
    );
}

export default CbCountries;

