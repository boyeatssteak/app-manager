import React from 'react';

import Heading from '../components/Heading';

class Home extends React.Component {

  render() {
    return (
      <div className="container">
        <Heading headingType="main" itemType="home" itemDisplayName="App Manager" />
        <section className="am-container">
          <p>This is a place to help you figure out what applications are on what servers, or what URL is used to manage ___ app.</p>
        </section>
      </div>
    );
  }
}

export default Home;