import React, { Component } from 'react';
import DisplayCheeseForm from './CheeseForm.js'
export class FetchCheeses extends Component {
  static displayName = FetchCheeses.name;
   state = {
     cheeselist: [],
     order:[],
      total:0,
      loading: true
    };

  componentDidMount() {
    this.populateData();
  }

  onChangeForm = (e, c) => {
    let lis = this.state.cheeselist;
    let cheese = lis.find((e) => e.id === c.id);
    cheese.cost = cheese.pricePerKilo / 1000 * e.target.value
    this.setState({ total: this.state.cheeselist.reduce(function(p, c) { return p + +c.cost},0)})
  }

  async addToOrder (e,cheese,g) {
    e.preventDefault();
      console.log(g.current.value);
    cheese.amount = g.current.value;
    let sendC = {
      Id:0,
      CheeseId: cheese.id,
      Amount: g.current.value,
      Cost: cheese.cost
    };
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(sendC)
    };
        const response = await fetch('/api/cheeseorder',requestOptions);
        return await response.json();
  }

   renderTable(cheeselist) {
     let total = this.state.total;
     let order = this.state.order;
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
          {cheeselist.map((t,index) =>
            <DisplayCheeseForm onChangeForm={this.onChangeForm} addToOrder={this.addToOrder} cheeseInfo={t} key={index}/>
          )}
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
      : this.renderTable(this.state.cheeselist);

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
    console.log(order);
  }
}
