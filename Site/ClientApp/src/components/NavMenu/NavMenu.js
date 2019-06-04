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
            <header class="header_area sticky-header">
                <div class="main_menu">
                    <nav class="navbar navbar-expand-lg navbar-light main_box">
                        <div class="container">
                            <NavbarBrand className="navbar-brand logo_h" tag={Link} to="/"><img src="img/logo.png" alt=""></img></NavbarBrand>
                            <div class="collapse navbar-collapse offset" id="navbarSupportedContent">
                                <ul class="nav navbar-nav menu_nav ml-auto">
                                    <li class="nav-item active"><NavItem><NavLink tag={Link} className="text-dark" to="/">Home</NavLink></NavItem></li>
                                    <li class="nav-item submenu dropdown">
                                        <a class="nav-link dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Shop</a>
                                        <ul class="dropdown-menu">
                                            <li class="nav-item"><NavItem><NavLink tag={Link} to="/fetch-data">Shop Category</NavLink></NavItem></li>
                                        </ul>
                                    </li>
                                    <li class="nav-item submenu dropdown">
                                        <a class="nav-link dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Account</a>
                                        <ul class="dropdown-menu">
                                            <li class="nav-item"><NavItem><NavLink tag={Link} to="/login">Login</NavLink></NavItem></li>
                                            <li class="nav-item"><NavItem><NavLink tag={Link} to="/counter">Register</NavLink></NavItem></li>
                                        </ul>
                                    </li>
                                </ul>
                                <ul class="nav navbar-nav navbar-right">
                                    <li class="nav-item"><NavItem><NavLink tag={Link} to="/bag"><span class="ti-bag"></span></NavLink></NavItem></li>
                                </ul>
                            </div>
                        </div>
                    </nav>
                </div>
            </header>
        );
    }
}
