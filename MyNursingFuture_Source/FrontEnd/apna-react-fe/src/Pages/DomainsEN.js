import React from 'react';
import AppPage from './AppPage';

const DomainsEN = () => {
  
  let hardContent = [
    {
      type: 'DOMAINLISTTITLE',
      framework: 'en'
    },
    {
      type: 'DOMAINLINKLIST',
      framework: 'en'
    }
  ]
  return (
    <AppPage hardContent={hardContent} title="Domains" />
  )
}
  

export default DomainsEN;



