import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Cheese</h1>
        <p>Welcome to your new single-page application, built with: react and dotnet core 6.0</p>

        <p>You must be here to buy some cheese</p>
      </div>
    );
  }
}
