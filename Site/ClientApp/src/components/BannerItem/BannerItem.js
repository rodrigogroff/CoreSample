import React, { Component } from 'react';

export class BannerItem extends Component {
    render() {
        return (
            <section className="banner-area">
                <div className="container">
                    <div className="row fullscreen align-items-center justify-content-start banner-content">
                        <div className="col-lg-5 col-md-6">
                            <h1>Nike New <br />Collection!</h1>
                            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et
								dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation.</p>
                            <div className="add-bag d-flex align-items-center">
                                <a className="add-btn" href=""><span class="lnr lnr-cross"></span></a>
                                <span className="add-text text-uppercase">Add to Bag</span>
                            </div>
                        </div>
                        <div className="col-lg-7 col-md-6">
                            <div className="banner-img">
                                <img className="img-fluid" src="img/banner/banner-img.png" alt=""></img>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        );
    }
}
