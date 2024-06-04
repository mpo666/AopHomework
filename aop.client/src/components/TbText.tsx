
type TbTextProps = {
    id: string;
    label: string;
    value: string;
};


const TbText = (props: TbTextProps) => {
    return (
        <div>
            <label htmlFor={props.id}>{props.label}</label>
            <input type="text" id={props.id} value={props.value} />
        </div>
    );
}

export default TbText;
            