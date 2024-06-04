export default function CbCountries(props: object) {
    if (!Array.isArray(props.countries)) {
        props.countries = [];
    }
    return (
        <div>
            <label htmlFor={props.id} > Country: </label>
            <select id={props.id} onChange={props.onchange}>
                <option value="">please select</option>
                {props.countries.map(country => <option key={country.name} value={country.name}>{country.name}</option>)}
            </select>
        </div>
    );
}

