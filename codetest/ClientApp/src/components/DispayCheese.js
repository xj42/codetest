import React from "react";

const DisplayCheese = ({ cheeseInfo, cheese, deleteCheese }) => {

    let acheese = cheese.find(e => e.id == cheeseInfo.cheeseId);
    console.log(acheese);
    let grams = React.createRef();
    return (
        <tr key={acheese.id}>
            <td>{acheese.name} <br />{acheese.colour}</td>
            <td><img src={acheese.image} alt="cheddar" height="100px" /></td>
            <td>${acheese.pricePerKilo}/1kg</td>
            <td>{cheeseInfo.amount} grams</td>
            <td>${cheeseInfo.cost}</td>
            <td><button onClick={(e)=> deleteCheese(e,cheeseInfo.id)}>Delete</button></td>
      </tr>
    )
}

export default DisplayCheese;

