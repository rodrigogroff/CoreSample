import React, { Component } from 'react';

import { ApiLocation } from '../../shared/ApiLocation'

export class Register extends Component {

    state = {
        Name: '',
        Email: '',
        Phone: '',
        Password: '',
        error: '',
        loading: false,
    }

    emailChangeHandler = e => { this.setState({ Email: e.target.value }) }
    nameChangeHandler = e => { this.setState({ Name: e.target.value }) }
    phoneChangeHandler = e => { this.setState({ Phone: e.target.value }) }
    passwordChangeHandler = e => { this.setState({ Password: e.target.value }) }

    executeRegister = e =>
    {
        e.preventDefault();

        const Name = this.state.Name;
        const Email = this.state.Email;
        const Phone = this.state.Phone;
        const Password = this.state.Password;

        var newAccData = JSON.stringify({ Name, Email, Phone, Password });

        this.setState({ loading: true });
        this.setState({ error: '' });

        fetch(ApiLocation.api_host + ':' + ApiLocation.api_port + ApiLocation.api_portal + 'createAccount', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: newAccData
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
                                    <h4>Please enter your credentials</h4>
                                </div>
                            </div>
                        </div>
                        <div className="col-lg-6 top">
                            <div className="login_form_inner">
                                <h3>Save your credentials:</h3>
                                <form onSubmit={this.executeRegister} className="row login_form">
                                    <div className="col-md-12 form-group">
                                        <input type="text" class="form-control" value={this.state.Email} onChange={this.emailChangeHandler} placeholder="Email" />
                                    </div>                                    
                                    <div className="col-md-12 form-group">
                                        <input type="text" class="form-control" value={this.state.Name} onChange={this.nameChangeHandler} placeholder="Full Name" />
                                    </div>
                                    <div className="col-md-12 form-group">
                                        <input type="text" class="form-control" value={this.state.Phone} onChange={this.phoneChangeHandler} placeholder="Phone" />
                                    </div>
                                    <div className="col-md-12 form-group">
                                        <input type="password" class="form-control" value={this.state.Password} onChange={this.passwordChangeHandler} placeholder="Password" />
                                    </div>
                                    <div className="col-md-12 form-group">
                                        <br />
                                        <br />
                                        <br />
                                        <button type="submit" value="login" onClick={this.executeRegister} className="primary-btn">Create Account!</button>
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
