import { Environment } from '@abp/ng.core';

const baseUrl = 'https://dev.app.stratformx.com';

export const environment = {
    production: false,
    application: {
        baseUrl,
        name: 'Saturn',
        logoUrl: '',
    },
    oAuthConfig: {
        issuer: 'https://dev.api.stratformx.com/',
        redirectUri: baseUrl,
        clientId: 'Saturn_App',
        scope: 'offline_access Saturn',
        requireHttps: true,
    },
    apis: {
        default: {
            url: 'https://dev.api.stratformx.com',
            rootNamespace: 'Saturn',
        },
    },
} as Environment;
