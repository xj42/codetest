import React, { Component } from 'react';
import DisplayCheese from './DispayCheese.js'
export class OrderDetails extends Component {
  static displayName = OrderDetails.name;
  state = {
    cheeselist: [],
     order:[],
    loading: true,
      total:0
    };

  componentDidMount() {
    this.populateData();

  }


  async DeleteFromOrder(e, g) {
    e.preventDefault();

  let sendC = {
    Id:g,
  };
  const requestOptions = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(sendC)
  };
    const response = await fetch('/api/cheeseorder/delete',requestOptions);
     await response.json();

    console.log("here")
    window.location.reload(false);
  }

  renderTable(order) {
    let total = this.state.total;
    return (
      <form>
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Name</th>
            <th>Image</th>
              <th></th>
            <th></th>
              <th></th>
              <th></th>
          </tr>
        </thead>
        <tbody>
            {order.map((t, index) => <DisplayCheese cheeseInfo={t}
              updateCheese={this.updateOrder}
              deleteCheese={this.DeleteFromOrder}
              cheese={this.state.cheeselist} key={index} />)}
        </tbody>
        <tfoot>
          <tr>
            <td colSpan={3}></td>
            <td style={{textAlign:"right"}} >Total</td>
            <td>${total}</td>
              <td></td>
          </tr>
        </tfoot>
        </table>
      </form>

    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : this.renderTable(this.state.order);

    return (
      <div>
        <img height={"100px"} alt="homer" style={{ float: "right"}}  src="/images/homer.jpeg" />
        <h1 id="tabelLabel" >CheeseList</h1>
        {contents}
      </div>
    );
  }

  async populateData() {
    const response = await fetch('/api/cheese');
    const data = await response.json();

    const orders = await fetch('/api/cheeseorder');
    let order = await orders.json();
    this.setState({ cheeselist: data, order: order, loading: false });
    this.setState({ total: this.state.order.reduce(function(p, c) { return p + +c.cost},0)})
  }


}
