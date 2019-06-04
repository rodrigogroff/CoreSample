import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home/Home';
import { FetchData } from './components/FetchData/FetchData';
import { Counter } from './components/Counter/Counter';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
        <Layout>
            <Switch>
                <Route exact path='/' component={Home} />
                <Route path='/counter' component={Counter} />
                <Route path='/teste' component={Counter} />
                <Route path='/fetch-data' component={FetchData} />
            </Switch>
      </Layout>
    );  
  }
}
