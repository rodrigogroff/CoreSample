import React, { Component } from 'react';

import { BannerItem } from '../../components/BannerItem/BannerItem';

export class Home extends Component {
    static displayName = Home.name;
    render() {
        return (
            <BannerItem></BannerItem>
        );
    }
}
