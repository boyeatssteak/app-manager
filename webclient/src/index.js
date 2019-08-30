import React from 'react';
import { render } from 'react-dom';
import { BrowserRouter as Router, Route } from 'react-router-dom';

// components
import Header from './components/Header';
import Applications from './screens/Applications';
import AppDetail from './screens/AppDetail';
import Home from './screens/Home';
import Instances from './screens/Instances';
import InstanceDetail from './screens/InstanceDetail';
import Platforms from './screens/Platforms';
import PlatformDetail from './screens/PlatformDetail';
import Search from './screens/Search';
import SecureAreas from './screens/Secure';
import SecureAreaDetail from './screens/SecureDetail';
import Servers from './screens/Servers';
import ServerDetail from './screens/ServerDetail';

class Index extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      hasLoaded: false,
      hasErrored: false
    }
  }

  fetchData ( url, that ) {
    that.setState({ hasLoaded: false });
    try {
      fetch( url )
      .then((response) => {
        if (!response.ok) {
          throw Error(response.statusText);
        }
        return response;
      })
      .then((response) => response.json())
      .then(
        (items) => {
        that.setState({ 
          response: items,
          hasLoaded: true 
        });
      })
    } catch(error) {
      console.error(error);
      that.setState({ hasErrored: true })
    }
  }

  render() {
    return (
      <Router>
        <div className="wrap">
          <Header />
          <Route exact path="/" component={Home} />
          <Route exact path="/apps" render={(props) => <Applications {...props} fetchData={this.fetchData} /> } />
          <Route exact path="/apps/:appId" render={(props) => <AppDetail {...props} fetchData={this.fetchData} />} />
          <Route exact path="/instances" render={(props) => <Instances {...props} fetchData={this.fetchData} /> } />
          <Route exact path="/instances/:instanceId" render={(props) => <InstanceDetail {...props} fetchData={this.fetchData} />} />
          <Route exact path="/platforms" render={(props) => <Platforms {...props} fetchData={this.fetchData} />} />
          <Route exact path="/platforms/:platformId" render={(props) => <PlatformDetail {...props} fetchData={this.fetchData} />} />
          <Route exact path="/secure" render={(props) => <SecureAreas {...props} fetchData={this.fetchData} />} />
          <Route exact path="/secure/:secureId" render={(props) => <SecureAreaDetail {...props} fetchData={this.fetchData} />} />
          <Route exact path="/servers" render={(props) => <Servers {...props} fetchData={this.fetchData} />} />
          <Route exact path="/servers/:serverId" render={(props) => <ServerDetail {...props} fetchData={this.fetchData} />} />
          {/* <Route exact path="/users" render={(props) => <Users {...props} fetchData={this.fetchData} />} />
          <Route exact path="/users/:userId" render={(props) => <UserDetail {...props} fetchData={this.fetchData} />} />
          <Route exact path="/vendors" render={(props) => <Vendors {...props} fetchData={this.fetchData} />} />
          <Route exact path="/vendors/:vendorId" render={(props) => <VendorDetail {...props} fetchData={this.fetchData} />} /> */}
          <Route exact path="/search" component={Search} />
        </div>
      </Router>
    )
  }

}

render(
    <div>
      <Index />
    </div>,
  document.getElementById('root')
);