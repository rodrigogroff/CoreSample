import React, { Component } from 'react';
import { ApiLocation } from '../../shared/ApiLocation'

export class Login extends Component {

    constructor(props) {
        super(props);
        this.state = { forecasts: [], loading: true };
    }

    componentDidMount() {
        fetch(ApiLocation.api_host + ':' +
            ApiLocation.api_port +
            ApiLocation.api_portal +
            'product/1')
            .then(response => response.json())
            .then(data => {
                console.log(data);
            });
    }

    render() {
        return (
            <section className="login_box_area section_gap_top_75">
                <div className="container">
                    <div className="row">
                        <div className="col-lg-6">
                            <div className="login_box_img">
                                <img className="img-fluid" src="img/login.jpg" alt="" />
                                <div className="hover">
                                    <h4>New to our website?</h4>
                                    <div className="col-md-12 form-group">
                                        <button type="submit" value="createAccount" class="primary-btn">Create Account</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="login_form_inner">
                                <h3>Log in to enter</h3>
                                <div className="row login_form">
                                    <div className="col-md-12 form-group">
                                        <input type="text" class="form-control" id="name" name="name" placeholder="Email" />
                                    </div>
                                    <div className="col-md-12 form-group">
                                        <input type="password" class="form-control" id="name" name="name" placeholder="Password" />
                                    </div>
                                    <div className="col-md-12 form-group">
                                        <button type="submit" value="login" class="primary-btn">Log In</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        );
    }
}
