import React from 'react';
import AppPage from './AppPage';

const DomainsRN = () => {
  
  let hardContent = [
    {
      type: 'DOMAINLISTTITLE',
      framework: 'rn'
    },
    {
      type: 'DOMAINLINKLIST',
      framework: 'rn'
    }
  ]
  return (
    <AppPage hardContent={hardContent} title="Domains" />
  )
}
export default DomainsRN;
