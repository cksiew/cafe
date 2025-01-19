import React from 'react';
import { Link, useLocation, useParams } from 'react-router-dom';
import classes from './Breadcrumb.module.css';



interface BreadcrumbItem {
  to: string;
  label: string;
}

const Breadcrumb: React.FC = () => {
  const location = useLocation();
  const params = useParams<{ cafeId?: string }>();

  const pathnames = location.pathname.split('/').filter((x) => x);

  const breadcrumbs: BreadcrumbItem[] = pathnames.map((value, index) => {
    const to = `/${pathnames.slice(0, index + 1).join('/')}`;
    let label;

    switch (value) {
      case 'cafes':
        label = 'Cafes';
        break;
      case 'employees':
        label = 'Employees';
        break;
      case params.cafeId:
        label = `Employees in ${params.cafeId}`;
        break;
      default:
        label = value.charAt(0).toUpperCase() + value.slice(1);
    }

    return {
      to,
      label,
    };
  });

  return (
    <div className={classes.breadcrumb}>
      <nav aria-label="breadcrumb">
        <ul>
          <li>
            <Link to="/">Home</Link>
          </li>
          {breadcrumbs.map((breadcrumb, index) => {
            if (index < breadcrumbs.length - 2) {
              return (<li key={index}>
                <Link to={breadcrumb.to}>{breadcrumb.label}</Link>
              </li>)
            } else {
              return <span key={index}>{breadcrumb.label}</span>
            }
          })}
        </ul>
      </nav>
    </div>
  )
}

export default Breadcrumb;