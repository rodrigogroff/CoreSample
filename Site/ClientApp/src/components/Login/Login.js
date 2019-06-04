import React, { Component } from 'react';
import { NavbarBrand } from 'reactstrap';
import { Link } from 'react-router-dom';

import { ApiLocation } from '../../shared/ApiLocation'

export class Login extends Component {

    state = {
        email: '',
        password: '',
        error: '',
        loading: false,
    }

    emailChangeHandler = e => { this.setState({ email: e.target.value }) }
    passwordChangeHandler = e => { this.setState({ password: e.target.value }) }

    executeLogin = e =>
    {
        e.preventDefault();

        const Login = this.state.email;
        const Passwd = this.state.password;

        var loginData = JSON.stringify({ Login, Passwd });

        this.setState({ loading: true });
        this.setState({ error: '' });

        fetch(ApiLocation.api_host + ':' + ApiLocation.api_port + ApiLocation.api_portal + 'authenticate', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: loginData
        }).then((res) => {
            if (res.ok === true) {                
                this.setState({ loading: false });
            }
            else res.json().then((data) => {
                this.setState({ error: data.message });
                this.setState({ loading: false });
            });
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
                                        <NavbarBrand className="primary-btn btn-xs" tag={Link} to="/">Create Account</NavbarBrand>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="login_form_inner">
                                <h3>Log in to enter</h3>
                                <form onSubmit={this.executeLogin} className="row login_form">
                                    <div className="col-md-12 form-group">
                                        <input type="text" class="form-control" value={this.state.email} onChange={this.emailChangeHandler} placeholder="Email" />
                                    </div>
                                    <div className="col-md-12 form-group">
                                        <input type="password" class="form-control" value={this.state.password} onChange={this.passwordChangeHandler} placeholder="Password" />
                                    </div>
                                    <div className="col-md-12 form-group">
                                        <button type="submit" value="login" onClick={this.executeLogin} className="primary-btn">Log In</button>
                                    </div>
                                    <div className="col-md-12 form-group">
                                        <br />
                                        {
                                            this.state.loading === true ? 
                                                <div class="spinner-border text-info" role="status"><span class="sr-only">Loading...</span></div>
                                                :
                                                <label>{this.state.error}</label>
                                        }
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        );
    }
}
