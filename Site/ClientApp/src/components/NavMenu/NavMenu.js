import React, { Component } from 'react';
import { NavItem, NavLink, NavbarBrand } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);
        this.state = {
            collapsed: true
        };
    }

    render() {
        return (
            <header className="header_area sticky-header">
                <div className="main_menu">
                    <nav className="navbar navbar-expand-lg navbar-light main_box">
                        <div className="container">
                            <NavbarBrand className="navbar-brand logo_h" tag={Link} to="/"><img src="img/logo.png" alt=""></img></NavbarBrand>
                            <div className="collapse navbar-collapse offset" id="navbarSupportedContent">
                                <ul className="nav navbar-nav menu_nav ml-auto">
                                    <li className="nav-item active"><NavItem><NavLink tag={Link} className="text-dark" to="/">Home</NavLink></NavItem></li>
                                    <li className="nav-item submenu dropdown">
                                        <a className="nav-link dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Shop</a>
                                        <ul className="dropdown-menu">
                                            <li className="nav-item"><NavItem><NavLink tag={Link} to="/fetch-data">Shop Category</NavLink></NavItem></li>
                                        </ul>
                                    </li>
                                    <li className="nav-item submenu dropdown">
                                        <a className="nav-link dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Account</a>
                                        <ul className="dropdown-menu">
                                            <li className="nav-item"><NavItem><NavLink tag={Link} to="/login">Login</NavLink></NavItem></li>
                                            <li className="nav-item"><NavItem><NavLink tag={Link} to="/register">Register</NavLink></NavItem></li>
                                        </ul>
                                    </li>
                                </ul>
                                <ul className="nav navbar-nav navbar-right">
                                    <li className="nav-item"><NavItem><NavLink tag={Link} to="/bag"><span class="ti-bag"></span></NavLink></NavItem></li>
                                </ul>
                            </div>
                        </div>
                    </nav>
                </div>
            </header>
        );
    }
}
