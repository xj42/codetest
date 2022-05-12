import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import './custom.css'
import { FetchCheeses } from './components/FetchCheeses';
import { OrderDetails } from './components/OrderDetails';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/fetch-cheese' component={FetchCheeses} />
        <Route path='/order-cheese' component={OrderDetails} />
      </Layout>
    );
  }
}
