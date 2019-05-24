import React, { Component } from 'react';
import { ApiLocation } from '../shared/ApiLocation'

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor (props) {
    super(props);
    this.state = { forecasts: [], loading: true };      
    }

    componentDidMount() {
        fetch(ApiLocation.api_host + ':' +
            ApiLocation.api_port +
            ApiLocation.api_portal +
            'categories?skip=0&take=10')
            .then(response => response.json())
            .then(data => {
                this.setState({ forecasts: data.list, loading: false });
            });
    }

  static renderForecastsTable (forecasts) {
    return (
      <table className='table table-striped'>
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map(forecast =>
            <tr key={forecast.Id}>
              <td>{forecast.Id}</td>
              <td>{forecast.Name}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render () {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.forecasts);

    return (
      <div>
        <h1>Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }
}
