import React from "react";


const DisplayCheeseForm = ({ onChangeForm, addToOrder, cheeseInfo }) => {
    let grams = React.createRef();
    return (
        <tr key={cheeseInfo.id}>
            <td>{cheeseInfo.name} <br />{ cheeseInfo.colour}</td>
            <td><img src={cheeseInfo.image} alt="cheddar" height="100px" /></td>
            <td>${cheeseInfo.pricePerKilo}/1kg</td>
            <td><input ref={grams} type={"text"} name="price" onChange={(e) => onChangeForm(e, cheeseInfo)}  placeholder={"100"}/> grams</td>
            <td>${cheeseInfo.cost}</td>
            <td><button onClick={(e)=> addToOrder(e,cheeseInfo, grams)}>Add</button></td>
        </tr>
    )
}

export default DisplayCheeseForm;

