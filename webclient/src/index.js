import React from 'react';
import { render } from 'react-dom';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import { Provider, connect } from 'react-redux';
import { combineReducers, createStore } from 'redux';

// components
import Header from './components/Header';
import Home from './screens/Home';
import Servers from './screens/Servers';
import Applications from './screens/Applications';
import Platforms from './screens/Platforms';
import Search from './screens/Search';

let initState = {
  stuff: 'No?'
}

function reducer (state = initState, { type, payload }) {
  switch (type) {
    default:
      console.log("case default");
      return state;
  }
}

const store = createStore(
  reducer,
  {
    stuff: 'Yes?'
  },
  window.devToolsExtension && window.devToolsExtension()
);

const Index = ({ store }) => (
  <Router>
    <div className="wrap">
      <Header />
      <Route exact path="/" component={Home} />
      <Route exact path="/servers" component={Servers} />
      <Route exact path="/apps" component={Applications} />
      <Route exact path="/platforms" component={Platforms} />
      <Route exact path="/search" component={Search} />
    </div>
  </Router>
);

render(
  <Provider store={store}>
    <div>
      <Index />
    </div>
  </Provider>,
  document.getElementById('root')
);