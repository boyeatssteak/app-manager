import React from 'react';
import { render } from 'react-dom';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import { Provider, connect } from 'react-redux';
import { combineReducers, createStore } from 'redux';
import registerServiceWorker from './registerServiceWorker';

// components
import Header from './components/Header';
import Home from './screens/Home';

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

registerServiceWorker();