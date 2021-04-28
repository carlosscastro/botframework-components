// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

import { Channels } from 'botbuilder';
import { Expression } from 'adaptive-expressions';
import { OnInvokeActivity } from 'botbuilder-dialogs-adaptive';
import { TurnPath } from 'botbuilder-dialogs';

/**
 * Actions triggered when a Teams InvokeActivity is received with activity.name='composeExtension/submitAction'
 * and activity.value.botMessagePreviewAction == 'edit'.
 */
export class OnTeamsMEBotMessagePreviewEdit extends OnInvokeActivity {
  static $kind = 'Teams.OnMEBotMessagePreviewEdit';

  public commandId?: string;

  /**
   * Create expression for this condition.
   *
   * @returns {Expression} An [Expression](xref:adaptive-expressions.Expression) used to evaluate this rule.
   */
  protected createExpression(): Expression {
    const expressions = [
      Expression.parse(
        `${TurnPath.activity}.channelId == '${Channels.Msteams}' && ${TurnPath.activity}.name == 'composeExtension/submitAction' && ` +
          `${TurnPath.activity}.value.botMessagePreviewAction == 'edit'`
      ),
      super.createExpression(),
    ];

    if (this.commandId) {
      expressions.push(
        Expression.parse(
          `${TurnPath.activity}.value.commandId == '${this.commandId}'`
        )
      );
    }

    return Expression.andExpression(...expressions);
  }
}
