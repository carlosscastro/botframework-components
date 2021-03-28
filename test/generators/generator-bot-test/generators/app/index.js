// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

'use strict';

const { BaseGenerator } = require('@microsoft/generator-bot-adaptive');

module.exports = class extends BaseGenerator {
  initializing() {
    this.composeWith(
      require.resolve('@microsoft/generator-bot-adaptive/generators/app'),
      Object.assign(this.options, {
        arguments: this.args,
        applicationSettingsDirectory: 'settings',
        packageReferences: [
          {
            name: 'BotFramework.Components.TestComponents',
            version: '1.0.0',
            isPlugin: true,
          },
        ],
        modifyApplicationSettings: (appSettings) => {
          appSettings.runtimeSettings.adapters = [
            {
              name: 'BotFramework.Components.TestComponents.SimpleHttpAdapter',
              route: 'adaptertest',
            },
          ];
        },
      })
    );
  }

  writing() {
    this._copyBotTemplateFiles();
  }
};
